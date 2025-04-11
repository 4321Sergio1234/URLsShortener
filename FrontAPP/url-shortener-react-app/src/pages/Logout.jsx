import { redirect } from "react-router-dom";

export function logout(){
    localStorage.removeItem('userData');
    return redirect('/');
}