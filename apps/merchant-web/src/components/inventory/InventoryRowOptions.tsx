import { useRef, useEffect } from "react";
import "@/styles/inventory/inventory-row-options.css";

interface InventoryRowOptionsProps {
  isVisible: boolean;
  setIsVisible: (visible: boolean) => void;
  onRemove: () => void;
  onEdit: () => void;
  onConfiguringSettings: () => void;
  isOwnerOption: boolean;
}

export const InventoryRowOptions = ({
  isVisible,
  setIsVisible,
  onRemove,
  onEdit,
  onConfiguringSettings,
  isOwnerOption,
}: InventoryRowOptionsProps) => {
  const menuRef = useRef<HTMLUListElement | null>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setIsVisible(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return (
    <ul
      ref={menuRef}
      className={`admin-inventory-row-options-ctn ${
        isVisible ? "show-inventory-row-options" : ""
      } ${
        isOwnerOption
          ? "admin-inventory-for-owner"
          : "admin-inventory-for-not-owner"
      }`}
    >
      {isOwnerOption ? (
        <>
          <li onClick={onConfiguringSettings}>Stock Threshold</li>
          <li onClick={onEdit} className="hiddable-inventory-option">
            Edit
          </li>
          <li onClick={onRemove} className="hiddable-inventory-option">
            Remove
          </li>
        </>
      ) : (
        <li onClick={onEdit} className="hiddable-inventory-option">
          Edit
        </li>
      )}
    </ul>
  );
};
