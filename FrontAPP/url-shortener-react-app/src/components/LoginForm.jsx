import {Form, Link, useActionData, useSearchParams} from 'react-router-dom';
import classes from './LoginForm.module.css';

export default function LoginForm(){
    const [searchParams] = useSearchParams();
    const isLogin = searchParams.get('mode') === 'login';

    return (
        <div className={classes.container}>
            <h1 className={classes.heading}>
                {isLogin ? 'Log In' : 'Create New Account'}
            </h1>

            <Form method="post" className={classes.form}>
                {!isLogin && <div className={classes.formGroup}>
                    <label htmlFor="username" className={classes.label}>Username</label>
                    <input 
                        id="username" 
                        name="username" 
                        type="username" 
                        className={classes.input}
                        minLength={1}
                        maxLength={20}
                        required 
                    />
                </div>}

                <div className={classes.formGroup}>
                    <label htmlFor="email" className={classes.label}>Email</label>
                    <input 
                        id="email" 
                        name="email" 
                        type="email" 
                        className={classes.input}
                        required 
                    />
                </div>
                
                <div className={classes.formGroup}>
                    <label htmlFor="password" className={classes.label}>Password</label>
                    <input 
                        id="password" 
                        name="password" 
                        type="password" 
                        className={classes.input}
                        minLength={8} 
                        maxLength={20} 
                        required 
                    />
                </div>
                
                <div className={classes.actions}>
                    <Link 
                        to={`?mode=${isLogin ? 'register' : 'login'}`}
                        className={classes.modeSwitch}
                    >
                        {isLogin ? 'Create new account' : 'Existing user? Log in'}
                    </Link>
                    <button type="submit" className={classes.submitButton}>
                        {isLogin ? 'Log In' : 'Sign Up'}
                    </button>
                </div>
            </Form>  
        </div>
    );
}