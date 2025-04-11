import { useRouteError } from "react-router-dom";

export default function Error(){
    const error = useRouteError();
    
    return (
        <div>
            <h1>{error.status}</h1>
            <p>{error.data}</p>
        </div>
    );
}