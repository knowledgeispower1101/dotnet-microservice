interface MenuItemProps {
  onClick: () => void;
  label: string;
}

function MenuItem({ onClick, label }: MenuItemProps) {
  return (
    <div onClick={onClick} className="px-4 py-2 hover:bg-gray-100 transition font-semibold">
      {label}
    </div>
  );
}

export default MenuItem;
