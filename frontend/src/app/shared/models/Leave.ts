import { NumberValueAccessor } from "@angular/forms";

export interface LeaveDate {
  hours: number;
  date: Date;
}

export interface Leave {
  requestId: number,
  employeeId: number;
  leaveType: 'Casual' | 'Sick' | 'Annual' | 'LIEU';
  description: string;
  dates: LeaveDate[];
}