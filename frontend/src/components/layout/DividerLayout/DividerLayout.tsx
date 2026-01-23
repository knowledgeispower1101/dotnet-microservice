import { Container } from '@/components';
import type { ReactNode } from 'react';

type SideProps = {
  children: ReactNode;
};

const LeftSide = ({ children }: SideProps) => {
  return <div className="lex-none basis-47.5 min-w-0 mr-5 mb-5">{children}</div>;
};

const RightSide = ({ children }: SideProps) => {
  return <div className="flex-1 min-w-0">{children}</div>;
};

type DividerLayoutProps = {
  children: ReactNode;
};

const DividerLayout = ({ children }: DividerLayoutProps) => {
  return <Container className="flex gap-4">{children}</Container>;
};

DividerLayout.LeftSide = LeftSide;
DividerLayout.RightSide = RightSide;

export default DividerLayout;
