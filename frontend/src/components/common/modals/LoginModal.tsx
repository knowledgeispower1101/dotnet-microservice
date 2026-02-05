import { type FieldValues, type SubmitHandler, useForm } from 'react-hook-form';
import type { ApiResponse, LoginResponse, LoginPayload } from '@/services';
import { loginModalStore, useAuthStore, type User } from '@/store';
import toast from 'react-hot-toast';
import Modal from './Modal';
import Heading from '../Heading';
import { Input } from '../input';
import { authHooks } from '@/hooks';

function LoginModal() {
  const loginModal = loginModalStore();
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
      onClose={loginModal.onClose}
      onSubmit={handleSubmit(onSubmit)}
      body={bodyContent}
      onResetForm={reset}
    />
  );
}

export default LoginModal;
