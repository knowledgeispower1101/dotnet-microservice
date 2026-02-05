import { type FieldValues, type SubmitHandler, useForm } from 'react-hook-form';
import type { ApiResponse, LoginResponse, LoginPayload } from '@/services';
import { loginModalStore, registerModalStore, useAuthStore, type User } from '@/store';
import toast from 'react-hot-toast';
import Modal from './Modal';
import Heading from '../Heading';
import { Input } from '../input';
import { authHooks } from '@/hooks';
import { useCallback } from 'react';

function LoginModal() {
  const loginModal = loginModalStore();
  const registerModal = registerModalStore();
  const { setAuth } = useAuthStore();
  const { useLogin } = authHooks;
  const { mutate: loginAction, isPending } = useLogin();
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<FieldValues>({
    defaultValues: {
      email: '',
      password: '',
    },
  });
  const toggle = useCallback(() => {
    loginModal.onClose();
    registerModal.onOpen();
  }, [loginModal, registerModal]);

  const footerContent = (
    <div className="flex flex-col gap-4 mt-3">
      <div className="text-neutral-500 text-center mt-4 font-light">
        <div className="justify-center flex flex-row items-center gap-2">
          <div>First time using Airbnb?</div>
          <div className="text-neutral-800 cursor-pointer hover:underline" onClick={toggle}>
            Create an account
          </div>
        </div>
      </div>
    </div>
  );

  const onSubmit: SubmitHandler<FieldValues> = (data) => {
    loginAction(data as LoginPayload, {
      onSuccess: ({
        data: { firstName, lastName, email, userId, accessToken },
        message,
      }: ApiResponse<LoginResponse>) => {
        toast.success(message ?? 'Login successfully');
        loginModal.onClose();
        const user: User = {
          firstName,
          lastName,
          email,
          id: userId,
        };
        setAuth(accessToken, user);
        reset();
      },
      onError: (error: { message: string }) => {
        toast.error(error.message || 'Something went wrong');
      },
    });
  };

  const bodyContent = (
    <div className="flex flex-col gap-4">
      <Heading title={'Welcome back'} subtitle={'Login to your account'} />
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
      isOpen={loginModal.isOpen}
      title={'Login'}
      actionLabel={'Continue'}
      footer={footerContent}
      onClose={loginModal.onClose}
      onSubmit={handleSubmit(onSubmit)}
      body={bodyContent}
      onResetForm={reset}
    />
  );
}

export default LoginModal;
