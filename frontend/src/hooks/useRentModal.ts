import { useMemo, useState } from 'react';
import { rentModalStore } from '@/store';
import { STEPS, type Steps } from '@/constants';

function useRentModal() {
  const rentModal = rentModalStore();
  const [step, setStep] = useState<Steps>(STEPS.CATEGORY);

  const onNext = () => {
    setStep((s) => Math.min(s + 1, STEPS.PRICE) as Steps);
  };

  const onBack = () => {
    setStep((s) => Math.max(s - 1, STEPS.CATEGORY) as Steps);
  };

  const actionLabel = useMemo(() => (step === STEPS.PRICE ? 'Create' : 'Next'), [step]);

  const secondaryActionLabel = useMemo(
    () => (step === STEPS.CATEGORY ? undefined : 'Back'),
    [step],
  );

  const handleOnClose = () => {
    rentModal.onClose();
    setStep(STEPS.CATEGORY);
  };
  return {
    rentModal,
    step,
    STEPS,

    onNext,
    onBack,
    actionLabel,
    secondaryActionLabel,
    handleOnClose,
  };
}
export default useRentModal;
