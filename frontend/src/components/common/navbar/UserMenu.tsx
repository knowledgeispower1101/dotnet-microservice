import { loginModalStore, registerModalStore, rentModalStore, useAuthStore } from '@/store';
import { useCallback, useEffect, useRef, useState } from 'react';
import { useQueryClient } from '@tanstack/react-query';
import { AiOutlineMenu } from 'react-icons/ai';
import Avatar from '../avatar/Avatar';
import { Loading } from '../Loading';
import { authHooks } from '@/hooks';
import MenuItem from './MenuItem';

function UserMenu() {
  const { user, clearAuth } = useAuthStore();
  const registerModal = registerModalStore();
  const loginModal = loginModalStore();
  const rentModal = rentModalStore();
  const [isOpen, setIsOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);
  const { mutate: logoutAction, isPending } = authHooks.useLogout();
  const queryClient = useQueryClient();

  const toggleOpen = useCallback(() => {
    setIsOpen((value) => !value);
  }, []);

  const logout = () => {
    logoutAction(undefined, {
      onSuccess: () => {
        clearAuth();
        queryClient.removeQueries({ queryKey: ['auth', 'me'] });
        setIsOpen(false);
      },
    });
  };
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const onRent = useCallback(() => {
    if (!user) return loginModal.onOpen();
    rentModal.onOpen();
  }, [user, loginModal, rentModal]);

  if (isPending) return <Loading />;
  return (
    <div className="relative" ref={menuRef}>
      <div className="flex flex-row items-center gap-3">
        <div
          onClick={onRent}
          className="hidden md:block text-sm font-semibold py-3 px-4 rounded-full hover:bg-neutral-100 transition cursor-pointer"
        >
          Airbnb your home
        </div>

        <div
          className="p-4 md:py-1 md:px-2 border border-neutral-200 flex grow items-center gap-3 rounded-full cursor-pointer hover:shadow-md transition"
          onClick={toggleOpen}
        >
          <AiOutlineMenu />
          <div className="hidden md:block">
            <Avatar />
          </div>
        </div>
      </div>

      {isOpen && (
        <div className="absolute rounded-xl shadow-md w-[40vw] md:w-3/4 bg-white overflow-hidden right-0 top-12 text-sm">
          <div className="flex flex-col cursor-pointer">
            {user ? (
              <>
                <MenuItem label="My trips" onClick={() => {}} />
                <MenuItem label="My favourites" onClick={() => {}} />
                <MenuItem label="My reservations" onClick={() => {}} />
                <MenuItem label="Airbnb my home" onClick={() => rentModal.onOpen} />
                <hr />
                <MenuItem label="Logout" onClick={logout} />
              </>
            ) : (
              <>
                <MenuItem
                  label="Login"
                  onClick={() => {
                    setIsOpen(false);
                    loginModal.onOpen();
                  }}
                />
                <MenuItem
                  label="Sign up"
                  onClick={() => {
                    setIsOpen(false);
                    registerModal.onOpen();
                  }}
                />
              </>
            )}
          </div>
        </div>
      )}
    </div>
  );
}

export default UserMenu;
