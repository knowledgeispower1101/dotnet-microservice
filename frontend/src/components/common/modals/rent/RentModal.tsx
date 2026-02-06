import { useForm, useWatch, type FieldValues } from 'react-hook-form';
import { CategoryStep, ImageUploadStep, InforStep, LocationStep } from './steps';
import { useRentModal } from '@/hooks';
import Modal from '../Modal';

function RentModal() {
  const form = useForm<FieldValues>({
    defaultValues: {
      category: '',
      location: null,
      guestCount: 1,
      roomCount: 1,
      bathroomCount: 1,
      imageSrc: null,
      price: 1,
      title: '',
      description: '',
    },
  });

  const { control, setValue, reset } = form;

  const {
    rentModal,
    step,
    STEPS,
    onNext,
    onBack,
    actionLabel,
    secondaryActionLabel,
    handleOnClose,
  } = useRentModal();

  const category = useWatch({ control, name: 'category' });
  const location = useWatch({ control, name: 'location' });
  const guestCount = useWatch({ control, name: 'guestCount' });
  const roomCount = useWatch({ control, name: 'roomCount' });
  const bathroomCount = useWatch({ control, name: 'bathroomCount' });

  const setCustomValue = (id: string, value: any) => {
    setValue(id, value, {
      shouldDirty: true,
      shouldTouch: true,
      shouldValidate: true,
    });
  };

  let bodyContent = (
    <CategoryStep category={category} onSelect={(value) => setCustomValue('category', value)} />
  );

  if (step === STEPS.LOCATION) {
    bodyContent = (
      <LocationStep
        location={location}
        onLocationChange={(value) => setCustomValue('location', value)}
      />
    );
  }
  if (step === STEPS.INFO) {
    bodyContent = (
      <InforStep
        guestCount={guestCount}
        roomCount={roomCount}
        bathroomCount={bathroomCount}
        onBathroomChange={(value) => setCustomValue('bathroomCount', value)}
        onGuestChange={(value) => setCustomValue('guestCount', value)}
        onRoomChange={(value) => setCustomValue('roomCount', value)}
      />
    );
  }

  if (step === STEPS.IMAGE) {
    bodyContent = <ImageUploadStep />;
  }

  return (
    <Modal
      title="Airbnb your home"
      isOpen={rentModal.isOpen}
      onClose={handleOnClose}
      onSubmit={onNext}
      actionLabel={actionLabel}
      body={bodyContent}
      secondaryActionLabel={secondaryActionLabel}
      secondaryAction={step === STEPS.CATEGORY ? undefined : onBack}
      onResetForm={reset}
    />
  );
}

export default RentModal;
