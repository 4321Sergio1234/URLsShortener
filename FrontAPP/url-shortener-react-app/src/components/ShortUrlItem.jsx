import { Link ,useRouteLoaderData} from "react-router-dom";
import classes from "./ShortUrlItem.module.css"

export default function ShortUrlItem({id ,shortenedUrl, originalUrl}){
    const userData = useRouteLoaderData('root');

    return (
        <li className={classes['url-item']}>
            <div className={classes['url-content']}>
                <div className={classes['url-group']}>
                    <div className={[classes.url, classes['short-url']].join(' ')}>
                        <span className={classes['url-label']}>Коротке посилання:</span>
                        <a href={originalUrl} target="_blank" rel="noopener noreferrer" className={classes['url-link']}>
                            {shortenedUrl}
                        </a>
                    </div>
                    
                    <div className="url long-url">
                        <span className={classes['url-label']}>
                            Повне посилання:
                            </span>
                        <span className={classes['url-value']} title={originalUrl}>
                            {originalUrl.length > 50 ? originalUrl.substring(0, 50) + '...' : originalUrl}
                        </span>
                    </div>
                </div>
                { (userData && userData.isLogin) &&
                <div className={classes['url-actions']}>
                    <Link to={`${id}`} className={classes['details-button']}>Деталі</Link>
                </div>}
            </div>
        </li>
    );
}