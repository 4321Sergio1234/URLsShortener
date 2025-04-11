import LoginForm from '../components/LoginForm';
import { redirect, useActionData } from 'react-router-dom';
import { writeUserData, readUserData } from '../utils/localStorageUtil';

export default function Login(){
    const data = useActionData()

    return (
        <LoginForm></LoginForm>
    );
}

export async function authAction({request}){
    const searchParams = new URL(request.url).searchParams;
    const mode = searchParams.get('mode') || 'register';

    const data = await request.formData();

    let authData = {
        email: data.get('email'),
        password: data.get('password'),
    }

    if(data.get('username')){
        authData = {
            ...authData,
            userName: data.get('username')
        };
    }

    const response = await fetch('https://localhost:7224/Auth/'+mode,{
        method: 'POST',
        headers:{
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(authData)
    })    

    let res = await response.json();

    if(res.errors){
        return { error: res?.errors[0]}
    }

    if(res.error){
        return res.error
    }

    if(!response.ok){
        throw new Response(JSON.stringify("Opps... Registration problems :("), {status: response.status});
    }

    if(mode === 'login' && response.ok){
        res = {...res, isLogin: true}
        console.log(res);
        writeUserData(res);
        return redirect('/')
    }

    return redirect('/');
}