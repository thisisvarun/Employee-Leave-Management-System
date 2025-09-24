export interface LeaveDate {
  id: number;
  hours: number;
  date: Date;
}

export interface Leave {
  request_Id: number;
  employeeId: number;
  leaveType: number;
  description: string;
  leaveStatus: number;
  dates: LeaveDate[];
}
