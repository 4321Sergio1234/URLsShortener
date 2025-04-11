import { useRouteLoaderData, Form, useLoaderData, redirect } from 'react-router-dom';
import classes from './StaticContent.module.css';
import { readUserData } from '../utils/localStorageUtil';


export default function StaticContent() {
    const staticContent = useLoaderData();
    const userData = useRouteLoaderData('root');

    return (
        <div className={classes.container}>
            <Form method="put" 
                className={classes.form}
            >
                <input 
                    className={`${classes.heading} ${(userData && userData.isAdmin) ? classes.editable : ''}`}
                    name="title"
                    defaultValue={staticContent.title}
                    readOnly={!(userData && userData.isAdmin)}
                />
                
                <textarea 
                    className={`${classes.content} ${(userData && userData.isAdmin) ? classes.editable : ''}`}
                    name="content"
                    defaultValue={staticContent.content}
                    readOnly={!(userData && userData.isAdmin)}
                    rows={15}
                />

                {(userData && userData.isLogin) && (
                    <button type="submit" className={classes.saveButton}>
                        Зберегти зміни
                    </button>
                )}
            </Form>
        </div>
    );
}

export async function staticContentLoader(pageTag){
    const response = await fetch('https://localhost:7224/StaticContent/'+pageTag)

    if(!response.ok){

    }

    return await response.json();
}

export async function updateStaticContextAction({request, params, pageTag}){
    const data = await request.formData();

    const staticContent = {
        title: data.get('title'),
        content: data.get('content'),
    }

    console.log(staticContent);

    const response = await fetch('https://localhost:7224/StaticContent/'+pageTag,{
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + readUserData().token
        },
        body: JSON.stringify(staticContent)
    })

    if(!response.ok){

    }

    return redirect('/')
}