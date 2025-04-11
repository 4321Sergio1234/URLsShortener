import {useLoaderData} from 'react-router-dom';
import { readUserData } from '../utils/localStorageUtil';
import ShortUrlItemInfo from '../components/ShortUrlItemInfo';
import { redirect } from 'react-router-dom';


export default function ShortUrlInfo(){
    const shortUrl = useLoaderData();
    return (
        <ShortUrlItemInfo data={shortUrl}/>
    );
}



export async function shortUrlDeleteAction({ request, params }) {
    const response = await fetch(`https://localhost:7224/ShortUrl/delete?id=${params.id}`, {
      method: 'DELETE',
      headers: {
        "Authorization": 'Bearer ' + readUserData().token 
    }
    });

    console.log('m')

    if(!response.ok){
        throw new Response(JSON.stringify("Opps... Delete problems("), {status: response.status});
    }

    return redirect('/');
}

export async function shortUrlInfoLoader({requst, params}){
    const response = await fetch('https://localhost:7224/ShortUrl/'+params.id,
        {
            headers: {
                "Authorization": 'Bearer ' + readUserData().token 
            }
        });
        
    if(!response.ok){
        throw new Response(JSON.stringify("Opps..."), {status: response.status});
    }


    return await response.json();
}