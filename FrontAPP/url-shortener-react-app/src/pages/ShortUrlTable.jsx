import { Link, NavLink, useLoaderData, useRouteLoaderData } from "react-router-dom";
import ShortUrlItemList from "../components/ShortUrlItemList";

export default function ShortUrlTable(){
    const pageResult = useLoaderData();
    

    return (
            <ShortUrlItemList shortUrlItems={pageResult.pageResult}></ShortUrlItemList>
        );
}

export async function shortUrlLoader(params){
    const response = await fetch('https://localhost:7224/ShortUrl?PageNumber=1&PageSize=1000')
    
    if(!response.ok){

    }
    
    const res = await response.json();
    return res;
}