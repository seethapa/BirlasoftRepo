import { Routes } from '@angular/router';
import { Splash } from './pages/splash/splash';
import { Login } from './pages/login/login';
import { Signup } from './pages/signup/signup';
import { AuthLayout } from './core/layouts/auth-layout/auth-layout';
import { Home } from './pages/home/home';
import { authGuard } from './core/guards/auth.guard/auth.guard';
import { VotingComponent } from './pages/home/voting/voting.component';




export const routes: Routes = [
  { path: '',  component: Splash },
  { path: 'login',component: Login  },
  { path: 'signup', component: Signup },

  {
    path: '',
     component: AuthLayout,
      canActivate: [authGuard],
    children: [
          { path: 'home', component: Home },
          { path: 'voting', component: VotingComponent }
    ]
  }
  
];

