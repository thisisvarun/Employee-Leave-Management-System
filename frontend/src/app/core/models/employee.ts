// This interface matches the EMP.Designation table
export interface Designation {
  Designation_Id: number;
  Name: string;
}

// This interface matches the EMP.Team table
export interface Team {
  Team_Id: number;
  Name: string;
  Manager_Id: number;
}

// This interface matches the LEAVES.Leave table
export interface Leave {
  LeaveRequest_Id: number;
  Employee_Id: number;
  Leave_Type: 'Casual' | 'Sick' | 'Annual' | 'LIEU';
  Description?: string;
  Status: 'Pending' | 'Approved' | 'Rejected';
  Comment?: string;
  Dates: LeaveDate[]; // We'll nest the dates for convenience
}

// This interface matches the LEAVES.Dates table
export interface LeaveDate {
  Leave_Id: number;
  Hours: number;
  Date: string; // Using string for date to match form inputs and API formats
}

// This interface matches the EMP.Employee table
export interface Employee {
  Employee_Id: number;
  First_Name: string;
  Last_Name: string;
  Email: string;
  Phone?: string;
  Team_Id?: number;
  Salary: number;
  Designation_Id: number;
  Date_Of_Joining: string;
  Active: boolean;
  // We don't include PasswordHash on the frontend for security
}
