import DashboardIcon from '@material-ui/icons/Dashboard';
import Divider from '@material-ui/core/Divider';
import Drawer from '@material-ui/core/Drawer';
import Hidden from '@material-ui/core/Hidden';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import {makeStyles} from '@material-ui/core/styles';
import PeopleIcon from '@material-ui/icons/People';
import React from 'react';
import {useHistory} from 'react-router-dom';

import Booking from '../../../assets/images/Booking.png';
import {useUserContext} from '../../../features/user/UserContext';
import {selectCurrentUser} from '../../../features/user/userSlice';
import {useAppSelector} from '../../../app/Store';

const drawerWidth = 210;

const useStyles = makeStyles((theme) => ({
   root: {},
   drawer: {
      [theme.breakpoints.up('md')]: {
         width: drawerWidth,
         flexShrink: 0,
      },
   },
   drawerPaper: {
      width: drawerWidth,
   },
   logo: {
      [theme.breakpoints.up('md')]: {
         backgroundImage: '',
         marginTop: 10,
         marginBottom: 50,
         marginLeft: 40,
         width: '80px',
         height: '75px',
      },
   },
   listItem: {
      marginTop: 25,
      marginBottom: 25,
   },
   listItemIcon: {
      marginRight: -10,
      marginLeft: 5,
   },
}));

type MenuItem = {
   label: string;
   icon: JSX.Element;
   path: string;
};

type Props = {
   mobileOpen: boolean;
   onClose: () => void;
};

export const MainMenu: React.FC<Props> = (props: Props) => {
   const history = useHistory();
   const styles = useStyles();
   const user = useAppSelector(selectCurrentUser);
   const {logout} = useUserContext();

   const menuItems: MenuItem[] = [
      {
         label: 'Home',
         icon: <DashboardIcon />,
         path: '/',
      },
      {
         label: 'All Businesses',
         icon: <DashboardIcon />,
         path: '/user/business',
      },
      {
         label: 'My Bookings',
         icon: <DashboardIcon />,
         path: '/user/bookings',
      },
      {
         label: 'Profile',
         icon: <DashboardIcon />,
         path: '/user/profile',
      },
      {
         label: 'Create Business',
         icon: <DashboardIcon />,
         path: '/business/new',
      },
   ];

   const ownerItems: MenuItem[] = [
      {
         label: 'Businesses',
         icon: <DashboardIcon />,
         path: '/business',
      },
      {
         label: 'Bookings',
         icon: <DashboardIcon />,
         path: '/business/bookings',
      },
      {
         label: 'Time Slots',
         icon: <DashboardIcon />,
         path: '/business/timeslots',
      },
      {
         label: 'Employees',
         icon: <DashboardIcon />,
         path: '/business/employees',
      },
   ];

   const employeeItems: MenuItem[] = [
      {
         label: 'Bookings',
         icon: <DashboardIcon />,
         path: '/business/bookings',
      },
      {
         label: 'Time Slots',
         icon: <DashboardIcon />,
         path: '/business/timeslots',
      },
   ];

   const {mobileOpen} = props;

   const handleMenuItemClick = (menuItem: MenuItem) => {
      history.push(menuItem.path);
      if (mobileOpen) {
         props.onClose();
      }
   };

   const drawer = (
      <>
         <div className={styles.logo}>
            <img src={Booking} />
         </div>
         <Divider />
         <List>
            {menuItems.map((menuItem) => (
               <ListItem
                  className={styles.listItem}
                  button
                  key={menuItem.label}
                  onClick={() => handleMenuItemClick(menuItem)}
               >
                  <ListItemIcon className={styles.listItemIcon}>{menuItem.icon}</ListItemIcon>
                  <ListItemText primary={menuItem.label} />
               </ListItem>
            ))}
         </List>
         <Divider />
         <List>
            {user?.role === 'Owner' &&
               ownerItems.map((menuItem) => (
                  <ListItem
                     className={styles.listItem}
                     button
                     key={menuItem.label}
                     onClick={() => handleMenuItemClick(menuItem)}
                  >
                     <ListItemIcon className={styles.listItemIcon}>{menuItem.icon}</ListItemIcon>
                     <ListItemText primary={menuItem.label} />
                  </ListItem>
               ))}
            {user?.role === 'Employee' &&
               employeeItems.map((menuItem) => (
                  <ListItem
                     className={styles.listItem}
                     button
                     key={menuItem.label}
                     onClick={() => handleMenuItemClick(menuItem)}
                  >
                     <ListItemIcon className={styles.listItemIcon}>{menuItem.icon}</ListItemIcon>
                     <ListItemText primary={menuItem.label} />
                  </ListItem>
               ))}
         </List>

         <Divider />
      </>
   );

   return (
      <nav className={styles.drawer} aria-label="navigation">
         <Hidden smDown implementation="css">
            <Drawer
               classes={{
                  paper: styles.drawerPaper,
               }}
               variant="permanent"
               open
            >
               {drawer}
               <List>
                  <ListItem
                     className={styles.listItem}
                     button
                     onClick={() => {
                        window.location.href = window.location.href.substring(
                           0,
                           window.location.href.indexOf('/') + 1
                        );
                        logout();
                     }}
                  >
                     <ListItemIcon className={styles.listItemIcon}>
                        <PeopleIcon />
                     </ListItemIcon>
                     <ListItemText primary="Logout" />
                  </ListItem>
               </List>
            </Drawer>
         </Hidden>
      </nav>
   );
};
