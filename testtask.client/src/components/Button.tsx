import { ReactNode } from "react";

interface ButtonProps {
  children: ReactNode;
  color?: "primary" | "danger" | "success" | "secondary";
  isSmall?: boolean;
  onClick: () => void;
}

const Button = ({
  children,
  color = "primary",
  isSmall = false,
  onClick,
}: ButtonProps) => {
  let className = "btn btn-" + color;
  className += isSmall ? " btn-sm" : "";

  return (
    <button className={className} onClick={onClick}>
      {children}
    </button>
  );
};

export default Button;
