import { NavLink, Form, useRouteLoaderData } from 'react-router-dom';
import classes from './MainNavigation.module.css';

export default function MainNavigation(){
    const userData = useRouteLoaderData('root');
    
    

    return(    
        <nav className={classes.navigation}>
            <div className={classes.navLinks}>
                <NavLink 
                    to="" 
                    className={({isActive}) => 
                        isActive ? `${classes.link} ${classes.active}` : classes.link
                    }
                    end
                >
                    SortUrls
                </NavLink>
                
                <NavLink 
                    to="about" 
                    className={({isActive}) => 
                        isActive ? `${classes.link} ${classes.active}` : classes.link
                    }
                >
                    About
                </NavLink>
                {!(userData && userData.isLogin) ?
                <NavLink 
                    to="login?mode=login" 
                    className={({isActive}) => 
                        isActive ? `${classes.link} ${classes.active}` : classes.link
                    }
                >
                    Login
                </NavLink> :
                <Form action='/logout' method='post'>
                    <button>Logout</button>
                </Form>}
            </div>

            {(userData && userData.isLogin) &&
                <div className={classes.userInfo}>
                    <div>{userData.userName}</div>
                    <div>{userData.email}</div>
                </div>
            }
        </nav>
    );
}