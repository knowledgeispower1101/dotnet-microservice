import { AiOutlineMenu } from 'react-icons/ai';
import Avatar from '../avatar/Avatar';
import { useCallback, useEffect, useRef, useState } from 'react';
import MenuItem from './MenuItem';
import { loginModalStore, registerModalStore, useAuthStore } from '@/store';

function UserMenu() {
  const { user } = useAuthStore();
  const registerModal = registerModalStore();
  const loginModal = loginModalStore();
  const [isOpen, setIsOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);
  const toggleOpen = useCallback(() => {
    setIsOpen((value) => !value);
  }, []);

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

  return (
    <div className="relative" ref={menuRef}>
      <div className="flex flex-row items-center gap-3">
        <div className="hidden md:block text-sm font-semibold py-3 px-4 rounded-full hover:bg-neutral-100 transition cursor-pointer">
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
                <MenuItem label="Airbnb my home" onClick={() => {}} />
                <hr />
                <MenuItem label="Logout" onClick={() => {}} />
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
