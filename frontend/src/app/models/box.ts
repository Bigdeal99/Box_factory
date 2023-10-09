export interface Box {
  boxId?: number; // Unique identifier for the box
  boxName: string; // User-friendly name or label
  boxWeight: string; // Use string data type for consistency
}

export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}
