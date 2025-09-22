import { Routes } from '@angular/router';
import { LoginForm } from './pages/login-form/login-form';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { ManagerDashboard } from './pages/manager-dashboard/manager-dashboard';
import { HrDashboard } from './pages/hr-dashboard/hr-dashboard';
import { AddEmployee } from './components/add-employee/add-employee';

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
    path: 'hr/:id',
    component: HrDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'hr/:id/add-employee',
    component: AddEmployee,
    canActivate: [AuthGuard],
  },
];