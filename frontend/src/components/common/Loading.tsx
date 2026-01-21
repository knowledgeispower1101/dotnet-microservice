import { Loader2 } from 'lucide-react';

const Loading = ({ text = 'Loading...' }: { text?: string }) => {
  return (
    <div className="flex h-full min-h-50 flex-col items-center justify-center gap-2">
      <Loader2 className="h-8 w-8 animate-spin text-muted-foreground" />
      <p className="text-sm text-muted-foreground">{text}</p>
    </div>
  );
};

export { Loading };
