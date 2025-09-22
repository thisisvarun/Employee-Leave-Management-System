import { Routes } from '@angular/router';
import { LoginForm } from './pages/login-form/login-form';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { ManagerDashboard } from './pages/manager-dashboard/manager-dashboard';
import { ApplyLeaveComponent } from './components/apply-leave/apply-leave';
import { LeavesHistory } from './components/leaves-history/leaves-history';
import { HrDashboard } from './pages/hr-dashboard/hr-dashboard';
import { AddEmployee } from './components/add-employee/add-employee';
import { ManageEmployees } from './components/manage-employees/manage-employees';
import { ManageTeams } from './components/manage-teams/manage-teams';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginForm,
  },
  {
    path: 'login',
    component: LoginForm,
  },
  {
    path: 'employee-dashboard/:id',
    component: EmployeeDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    component: LoginForm,
  },
  {
    path: 'manager-dashboard/:id',
    component: ManagerDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'apply-leave/:id',
    component: ApplyLeaveComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'leaves-history/:id',
    component: LeavesHistory,
    canActivate: [AuthGuard]
  },
  {
    path: 'hr-dashboard/:id',
    component: HrDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'hr/:id',
    component: HrDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'hr/:id/add-employee',
    component: AddEmployee,
    canActivate: [AuthGuard],
  },
  {
    path: 'hr/:id/manage-employees',
    component: ManageEmployees,
    canActivate: [AuthGuard],
  },
  {
    path: 'hr/:id/manage-teams',
    component: ManageTeams,
    canActivate: [AuthGuard],
  },
];