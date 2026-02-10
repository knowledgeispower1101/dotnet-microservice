import { AiFillGithub } from 'react-icons/ai';
import { FcGoogle } from 'react-icons/fc';
import { type FieldValues, type SubmitHandler, useForm } from 'react-hook-form';
import Modal from './Modal';
import Heading from '../Heading';
import { Input } from '../input';
import toast from 'react-hot-toast';
import Button from '../Button';
import type { RegisterPayload } from '@/services/user';
import { loginModalStore, registerModalStore } from '@/store';
import type { ApiResponse } from '@/services';
import { authHooks } from '@/hooks';
import { useCallback } from 'react';

function RegisterModal() {
  const { useRegister } = authHooks;
  const loginModal = loginModalStore();
  const registerModal = registerModalStore();
  const { mutate: registerAction, isPending } = useRegister();

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<FieldValues>({
    defaultValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
    },
  });

  const onSubmit: SubmitHandler<FieldValues> = (data) => {
    registerAction(data as RegisterPayload, {
      onSuccess: (response: ApiResponse<string>) => {
        toast.success(response.message ?? 'Login successfully');
        registerModal.onClose();
        reset();
      },
      onError: (error) => {
        toast.error(error?.message || 'Something went wrong');
      },
    });
  };

  const toggle = useCallback(() => {
    registerModal.onClose();
    loginModal.onOpen();
  }, [loginModal, registerModal]);

  const footerContent = (
    <div className="flex flex-col gap-4 mt-3">
      <hr />
      <Button outline label="Continue with Google" icon={FcGoogle} onClick={() => {}} />
      <Button outline label="Continue with Github" icon={AiFillGithub} onClick={() => {}} />
      <div className="text-neutral-500 text-center mt-4 font-light">
        <div className="justify-center flex flex-row items-center gap-2">
          <div>Already have account?</div>
          <div className="text-neutral-800 cursor-pointer hover:underline" onClick={toggle}>
            Login
          </div>
        </div>
      </div>
    </div>
  );
  const bodyContent = (
    <div className="flex flex-col gap-4">
      <Heading title={'Welcome to Airbnb'} subtitle={'Create an account'} />
      <Input id={'firstName'} label={'First Name'} disabled={isPending} register={register} errors={errors} />
      <Input id={'lastName'} label={'Last Name'} disabled={isPending} register={register} errors={errors} />
      <Input id={'email'} label={'Email'} disabled={isPending} register={register} errors={errors} required />
      <Input
        id={'password'}
        label={'Password'}
        disabled={isPending}
        register={register}
        errors={errors}
        required
        type="password"
      />
    </div>
  );
  return (
    <Modal
      disabled={isPending}
      isOpen={registerModal.isOpen}
      title={'Register'}
      actionLabel={'Continue'}
      onClose={registerModal.onClose}
      onSubmit={handleSubmit(onSubmit)}
      body={bodyContent}
      footer={footerContent}
      onResetForm={reset}
    />
  );
}

export default RegisterModal;
