import { Routes } from '@angular/router';
import { LoginForm } from './pages/login-form/login-form';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { ManagerDashboard } from './pages/manager-dashboard/manager-dashboard';
import { ApplyLeaveComponent } from './components/apply-leave/apply-leave';
import { LeavesHistory } from './components/leaves-history/leaves-history';
import { HrDashboard } from './pages/hr-dashboard/hr-dashboard';
import { CreateEmployee } from './components/create-employee/create-employee';

export const routes: Routes = [
  {
    path: 'employee/:id',
    component: EmployeeDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    component: LoginForm,
  },
  {
    path: 'manager/:id',
    component: ManagerDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'apply-leave/:id',
    component: ApplyLeaveComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'leave-history/:id',
    component: LeavesHistory,
    canActivate: [AuthGuard]
  },
  {
    path: 'hr/:id',
    component: HrDashboard,
    canActivate: [AuthGuard]
  },
  {
    path: 'hr/:id/create-employee',
    component: CreateEmployee
  }
];
