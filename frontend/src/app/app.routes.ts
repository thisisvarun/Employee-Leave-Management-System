import { Routes } from '@angular/router';
import { LoginForm } from './pages/login-form/login-form';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { ManagerDashboard } from './pages/manager-dashboard/manager-dashboard';
import { ApplyLeaveComponent } from './components/apply-leave/apply-leave';
import { LeavesHistory } from './components/leaves-history/leaves-history';
import { HrDashboard } from './pages/hr-dashboard/hr-dashboard';

export const routes: Routes = [
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
  }
];
