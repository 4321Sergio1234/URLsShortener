import { redirect } from "react-router-dom";
import ShortUrlForm from "../components/ShortUrlForm";
import { readUserData } from "../utils/localStorageUtil";

export default function ShortUrlNew(){
    return (
        <ShortUrlForm></ShortUrlForm>
       
        );
}

export async function createShortUrlAction({request, params}){
    const data = await request.formData();

    const nShortUrl = {
        originalUrl: data.get('originalUrl'),
        userId: readUserData().id
    };
    
    const response = await fetch("https://localhost:7224/ShortUrl/shorten",{
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "authorization": "Bearer " + readUserData().token
        },
        body: JSON.stringify(nShortUrl)
    });

    if(!response.ok){
        throw new Response(JSON.stringify("Opps..."), {status: response.status});
    }

    let res = await response.json();

    if(res.error){
        throw new Response(JSON.stringify(res.error), {status: response.status});
    }

    return redirect('/');
}