import { AiFillGithub } from 'react-icons/ai';
import { FcGoogle } from 'react-icons/fc';
import { type FieldValues, type SubmitHandler, useForm } from 'react-hook-form';
import { useRegister, useRegisterModal } from '@/hooks';
import Modal from './Modal';
import Heading from '../Heading';
import { Input } from '../input';
import toast from 'react-hot-toast';
import Button from '../Button';
import type { RegisterPayload } from '@/services/user';
import type { ApiResponse } from '@/services';

function RegisterModal() {
  const registerModal = useRegisterModal();
  const { mutate: registerAction, isPending } = useRegister();

  // const [isLoading, setIsLoading] = useState(false);
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
      onSuccess: (data: ApiResponse<string>) => {
        toast.success(data.message ?? 'Register successfully');
        registerModal.onClose();
        reset();
      },
      onError: (error: any) => {
        toast.error(error?.response?.data?.message || 'Something went wrong');
      },
    });
  };

  const footerContent = (
    <div className="flex flex-col gap-4 mt-3">
      <hr />
      <Button outline label="Continue with Google" icon={FcGoogle} onClick={() => {}} />
      <Button outline label="Continue with Github" icon={AiFillGithub} onClick={() => {}} />
      <div className="text-neutral-500 text-center mt-4 font-light">
        <div className="justify-center flex flex-row items-center gap-2">
          <div>Already have account?</div>
          <div className="text-neutral-800 cursor-pointer hover:underline" onClick={registerModal.onClose}>
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
    />
  );
}

export default RegisterModal;
