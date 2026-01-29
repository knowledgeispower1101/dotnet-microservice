import type { ReactNode } from 'react';

type ContainerProps = {
  children: ReactNode;
};

const Container: React.FC<ContainerProps> = ({ children }) => {
  return <div className="max-w-630 max-auto xl:px-20 md:px-10 sm:px-2 px-4">{children}</div>;
};

export default Container;
