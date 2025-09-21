import { Routes } from '@angular/router';
import { LoginForm } from './pages/login-form/login-form';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { ManagerDashboard } from './pages/manager-dashboard/manager-dashboard';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginForm,
  },
  {
    path: 'employee/:id',
    component: EmployeeDashboard,
    canActivate: [AuthGuard],
  },
  {
    path: 'manager/:id',
    component: ManagerDashboard,
    canActivate: [AuthGuard],
  },
];
