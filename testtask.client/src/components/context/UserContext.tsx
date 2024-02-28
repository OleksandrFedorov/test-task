import { ReactNode, createContext, useState } from "react";

export type User = {
  id: string;
  name: string;
  password: string;
  sessionId: string;
};

export type UserSession = {
  id: string;
  lastLogin: string;
  user: User;
};

type SetValue = (value: User) => void;

export interface UserContextInterface {
  user: User;
  setUser: SetValue;
}

export const defaultState = {
  user: {
    id: "",
    name: "",
    password: "",
    sessionId: "",
  },
  setUser: () => {},
} as UserContextInterface;

export const UserContext = createContext<UserContextInterface>(defaultState);

type UserProviderProps = {
  children: ReactNode;
};

export const UserProvider: React.FC<UserProviderProps> = ({
  children,
}: UserProviderProps) => {
  const [user, setUser] = useState<User>({
    id: "",
    name: "",
    password: "",
    sessionId: "",
  });

  return (
    <UserContext.Provider value={{ user, setUser }}>
      {children}
    </UserContext.Provider>
  );
};
