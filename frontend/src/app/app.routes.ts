import { Routes } from '@angular/router';
import { LoginForm } from './pages/login-form/login-form';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { ManagerDashboard } from './pages/manager-dashboard/manager-dashboard';
import { LeavesHistory } from './components/leaves-history/leaves-history';

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
    path: 'leave-history/:id',
    component: LeavesHistory,
    canActivate: [AuthGuard],
  },
];
