import { useUpload } from '@/hooks';
import type { ImageItem, ImageUploadProps } from '@/services';
import { useState, useRef, type DragEvent, type ChangeEvent, useEffect } from 'react';
import { AiOutlineCloudUpload, AiOutlineClose } from 'react-icons/ai';

function ImageUploadStep({ setImage, onChange }: ImageUploadProps) {
  const [images, setImages] = useState<ImageItem[]>(setImage);
  const [draggedIndex, setDraggedIndex] = useState<number | null>(null);
  const [isDraggingOver, setIsDraggingOver] = useState<boolean>(false);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const { useUploadImages } = useUpload;
  const { mutate: uploadAction, isPending } = useUploadImages();
  useEffect(() => {
    onChange(images);
  }, [images]);

  const handleFileSelect = (e: ChangeEvent<HTMLInputElement>) => {
    const files = Array.from(e.target.files || []);
    handleFiles(files);
  };

  const handleFiles = (files: File[]) => {
    const imageFiles = files.filter((file) => file.type.startsWith('image/'));
    const newImages: ImageItem[] = imageFiles.map((file) => ({
      id: Math.random().toString(36).substr(2, 9),
      file,
      contentType: file.type,
      preview: URL.createObjectURL(file),
    }));

    uploadAction(imageFiles, {
      onSuccess: (data) => {
        console.log(data);
      },
      onError: (error) => {
        console.log(error);
      },
    });
    if (!isPending) setImages((prev) => [...prev, ...newImages]);
  };

  const handleDrop = (e: DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsDraggingOver(false);

    const files = Array.from(e.dataTransfer.files);
    handleFiles(files);
  };

  const handleDragOver = (e: DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsDraggingOver(true);
  };

  const handleDragLeave = (e: DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsDraggingOver(false);
  };

  const deleteImage = (id: string) => {
    setImages((prev) => {
      const updated = prev.filter((img) => img.id !== id);
      const imageToDelete = prev.find((img) => img.id === id);
      if (imageToDelete) {
        URL.revokeObjectURL(imageToDelete.preview);
      }
      return updated;
    });
  };

  const handleImageDragStart = (e: DragEvent<HTMLDivElement>, index: number) => {
    setDraggedIndex(index);
    e.dataTransfer.effectAllowed = 'move';
  };

  const handleImageDragOver = (e: DragEvent<HTMLDivElement>, index: number) => {
    e.preventDefault();
    e.dataTransfer.dropEffect = 'move';
  };

  const handleImageDrop = (e: DragEvent<HTMLDivElement>, dropIndex: number) => {
    e.preventDefault();

    if (draggedIndex === null || draggedIndex === dropIndex) return;

    const newImages = [...images];
    const draggedImage = newImages[draggedIndex];

    newImages.splice(draggedIndex, 1);
    newImages.splice(dropIndex, 0, draggedImage);

    setImages(newImages);
    setDraggedIndex(null);
  };

  const handleImageDragEnd = () => {
    setDraggedIndex(null);
  };

  const setAsMainImage = (index: number) => {
    if (index === 0) return;
    const newImages = [...images];
    const selectedImage = newImages[index];
    newImages.splice(index, 1);
    newImages.unshift(selectedImage);
    setImages(newImages);
  };

  const mainImage = images[0];
  const remainingImages = images.slice(1);

  return (
    <div className="flex flex-col gap-8">
      {images.length === 0 && (
        <div
          onClick={() => fileInputRef.current?.click()}
          onDrop={handleDrop}
          onDragOver={handleDragOver}
          onDragLeave={handleDragLeave}
          className={`border-2 border-dashed rounded-lg p-12 text-center cursor-pointer transition-all ${
            isDraggingOver
              ? 'border-blue-500 bg-blue-50'
              : 'border-gray-300 hover:border-gray-400 bg-gray-50'
          }`}
        >
          <AiOutlineCloudUpload className="mx-auto h-12 w-12 text-gray-400 mb-4" />
          <p className="text-lg text-gray-600 mb-2">Drag and drop images here</p>
          <p className="text-sm text-gray-500 mb-4">or</p>
          <button className="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors">
            Browse Files
          </button>
        </div>
      )}

      {/* Hidden File Input */}
      <input
        ref={fileInputRef}
        type="file"
        multiple
        accept="image/*"
        onChange={handleFileSelect}
        className="hidden"
      />

      {/* Images Display */}
      {images.length > 0 && (
        <div className="space-y-6">
          {/* Main Image */}
          <div className="space-y-2">
            <h3 className="text-lg font-semibold text-gray-700">Main Image</h3>
            <div
              className="relative group rounded-lg overflow-hidden bg-gray-200 aspect-video"
              draggable
              onDragStart={(e) => handleImageDragStart(e, 0)}
              onDragOver={(e) => handleImageDragOver(e, 0)}
              onDrop={(e) => handleImageDrop(e, 0)}
              onDragEnd={handleImageDragEnd}
            >
              <img
                src={mainImage.preview}
                alt="Main"
                className="w-full h-full object-cover"
                onError={() => console.error('Failed to load main image')}
              />
              <button
                onClick={() => deleteImage(mainImage.id)}
                className="absolute top-3 right-3 p-2 bg-red-500 text-white rounded-full hover:bg-red-600 transition-all opacity-0 group-hover:opacity-100 shadow-lg z-10"
              >
                <AiOutlineClose className="h-5 w-5" />
              </button>
              <div className="absolute bottom-3 left-3 px-3 py-1 bg-black bg-opacity-50 text-white text-sm rounded-md">
                Main
              </div>
            </div>
          </div>

          {/* Additional Images Grid */}
          {remainingImages.length > 0 && (
            <div className="space-y-2">
              <div className="flex justify-between items-center">
                <h3 className="text-lg font-semibold text-gray-700">
                  Additional Images ({remainingImages.length})
                </h3>
                <button
                  onClick={() => fileInputRef.current?.click()}
                  className="px-4 py-2 bg-blue-600 text-white text-sm rounded-lg hover:bg-blue-700 transition-colors flex items-center gap-2"
                >
                  <AiOutlineCloudUpload className="h-4 w-4" />
                  Add More
                </button>
              </div>

              <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                {remainingImages.map((image, idx) => {
                  const actualIndex = idx + 1;
                  return (
                    <div
                      key={image.id}
                      draggable
                      onDragStart={(e) => handleImageDragStart(e, actualIndex)}
                      onDragOver={(e) => handleImageDragOver(e, actualIndex)}
                      onDrop={(e) => handleImageDrop(e, actualIndex)}
                      onDragEnd={handleImageDragEnd}
                      className={`relative group rounded-lg overflow-hidden bg-gray-200 aspect-square cursor-move transition-all ${
                        draggedIndex === actualIndex ? 'opacity-50' : 'opacity-100'
                      }`}
                    >
                      <img
                        src={image.preview}
                        alt={`Image ${actualIndex}`}
                        className="w-full h-full object-cover"
                        onClick={() => setAsMainImage(actualIndex)}
                        onError={() => console.error(`Failed to load image ${actualIndex}`)}
                      />
                      <button
                        onClick={(e) => {
                          e.stopPropagation();
                          deleteImage(image.id);
                        }}
                        className="absolute top-2 right-2 p-1.5 bg-red-500 text-white rounded-full hover:bg-red-600 transition-all opacity-0 group-hover:opacity-100 shadow-lg z-10"
                      >
                        <AiOutlineClose className="h-4 w-4" />
                      </button>
                      <div className="absolute inset-0 bg-black bg-opacity-0 group-hover:bg-opacity-20 transition-all flex items-center justify-center pointer-events-none">
                        <p className="text-white text-sm opacity-0 group-hover:opacity-100 transition-opacity">
                          Click to set as main
                        </p>
                      </div>
                    </div>
                  );
                })}
              </div>
            </div>
          )}

          {/* Add More Button (when only main image exists) */}
          {images.length === 1 && (
            <button
              onClick={() => fileInputRef.current?.click()}
              className="w-full py-3 border-2 border-dashed border-gray-300 rounded-lg text-gray-600 hover:border-gray-400 hover:bg-gray-50 transition-all flex items-center justify-center gap-2"
            >
              <AiOutlineCloudUpload className="h-5 w-5" />
              Add More Images
            </button>
          )}
        </div>
      )}
    </div>
  );
}

export default ImageUploadStep;
