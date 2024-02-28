import { User } from "../context/UserContext";

export interface File {
  id: string;
  created: string;
  name: string;
  thumbnail: string;
  type: number;
  mimeType: string;
  path: string;
  size: number;
  numberOfDownloads: number;
  user: User;
  shares: FileShare[];
}

export interface FileShare {
  id: string;
  expired: string;
  created: string;
  file: File | null;
}
