import { useMemo, useState, lazy, Suspense } from 'react';
import { rentModalStore } from '@/store';
import { categories } from '@/constants';
import Heading from '../Heading';
import Modal from './Modal';
import { CategoryInput } from '../input';
import { useForm, useWatch, type FieldValues } from 'react-hook-form';
import CountrySelect from '../input/CountrySelect';

const STEPS = {
  CATEGORY: 0,
  LOCATION: 1,
  INFO: 2,
  IMAGE: 3,
  DESCRIPTION: 4,
  PRICE: 5,
} as const;

type Steps = (typeof STEPS)[keyof typeof STEPS];

const Map = lazy(() => import('../Map'));

function RentModal() {
  const rentModal = rentModalStore();
  const [step, setStep] = useState<Steps>(STEPS.CATEGORY);

  const onBack = () => {
    setStep((value) => Math.max(value - 1, STEPS.CATEGORY) as Steps);
  };

  const onNext = () => {
    setStep((value) => Math.min(value + 1, STEPS.PRICE) as Steps);
  };

  const { setValue, control, reset } = useForm<FieldValues>({
    defaultValues: {
      category: '',
      location: null,
      guestCount: 1,
      roomCount: 1,
      bathroomCount: 1,
      imageSrc: '',
      price: 1,
      title: '',
      description: '',
    },
  });

  const category = useWatch({ control, name: 'category' });
  const location = useWatch({ control, name: 'location' });

  const setCustomValue = (id: string, value: any) => {
    setValue(id, value, {
      shouldValidate: true,
      shouldDirty: true,
      shouldTouch: true,
    });
  };

  let bodyContent = (
    <div className="flex flex-col gap-8">
      <Heading title="Which of these best describes your place?" subtitle="Pick a category" />
      <div className="grid grid-cols-1 md:grid-cols-2 gap-3 max-h-[50vh] overflow-y-auto">
        {categories.map(({ icon, label }) => (
          <div key={label} className="col-span-1">
            <CategoryInput
              selected={category === label}
              onClick={(category) => setCustomValue('category', category)}
              icon={icon}
              label={label}
            />
          </div>
        ))}
      </div>
    </div>
  );

  const secondaryActionLabel = useMemo(() => {
    if (step === STEPS.CATEGORY) return undefined;
    return 'Back';
  }, [step]);

  const actionLabel = useMemo(() => {
    if (step === STEPS.PRICE) return 'Create';
    return 'Next';
  }, [step]);

  if (step === STEPS.LOCATION) {
    console.log(location?.latlng);
    bodyContent = (
      <div className="flex flex-col gap-8">
        <Heading title="Where is your place located?" subtitle="Help guests find you!" />
        <CountrySelect value={location} onChange={(value) => setCustomValue('location', value)} />
        <Suspense fallback={<div className="h-[35vh] rounded-lg bg-neutral-200" />}>
          <Map key={location?.value || 'default'} center={location?.latlng} />
        </Suspense>
      </div>
    );
  }

  const handleOnClose = () => {
    rentModal.onClose();
    setStep(STEPS.CATEGORY);
  };

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
