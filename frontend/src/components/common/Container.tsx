import type { ReactNode } from 'react';

type ContainerProps = {
  children: ReactNode;
  className?: string;
};

export const Container = ({ children, className }: ContainerProps) => {
  return <div className={`max-w-300 mx-auto ${className}`}>{children}</div>;
};
