import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Dashboard',
    icon: 'home-outline',
    link: '/pages/dashboard',
    home: true,
    data: {
      permission: 'view',
      resource: 'dashboard'
    },
  },
  {
    title: 'FEATURES',
    group: true,
  }, 
  {
    title: 'System Configuration',
    icon: 'lock-outline',
    link: '/configurate',
    data: {
      permission: 'view',
      resource: 'admin'
    },
  },
  {
    title: 'Social Configuration',
    icon: 'lock-outline',
    link: '/social-configuration',
    data: {
      permission: 'view',
      resource: 'admin'
    },
  },


];
