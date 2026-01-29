import { AiFillGithub } from 'react-icons/ai';
import { FcGoogle } from 'react-icons/fc';
import { type FieldValues, type SubmitHandler, useForm } from 'react-hook-form';
import { useRegisterModal } from '@/hooks';
import { useState } from 'react';
import Modal from './Modal';
import Heading from '../Heading';
import { Input } from '../input';

function RegisterModal() {
  const registerModal = useRegisterModal();
  const [isLoading, setIsLoading] = useState(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FieldValues>({
    defaultValues: {
      firstName: '',
      lastName: '',
      email: '',
      currentPassword: '',
    },
  });

  const onSubmit: SubmitHandler<FieldValues> = (data) => {
    setIsLoading(true);
    console.log(data);
    setIsLoading(false);
    // axios
    //   .post('/auth/register', data)
    //   .then(() => {

    //     registerModal.onClose();
    //   })
    //   .catch((error) => {
    //     console.log(error);
    //   })
    //   .finally(() => {
    //     setIsLoading(false);
    //   });
  };
  const bodyContent = (
    <div className="flex flex-col gap-4">
      <Heading title={'Welcome to Airbnb'} subtitle={'Create an account'} />

      <Input id={'firstName'} label={'First Name'} disabled={isLoading} register={register} errors={errors} />
      <Input id={'lastName'} label={'Last Name'} disabled={isLoading} register={register} errors={errors} />
      <Input id={'email'} label={'Email'} disabled={isLoading} register={register} errors={errors} required />
      <Input
        id={'currentPassword'}
        label={'Password'}
        disabled={isLoading}
        register={register}
        errors={errors}
        required
        type="password"
      />
    </div>
  );
  return (
    <Modal
      disabled={isLoading}
      isOpen={registerModal.isOpen}
      title={'Register'}
      actionLabel={'Continue'}
      onClose={registerModal.onClose}
      onSubmit={handleSubmit(onSubmit)}
      body={bodyContent}
    />
  );
}

export default RegisterModal;
