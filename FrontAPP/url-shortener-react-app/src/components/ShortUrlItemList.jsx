import classes from './ShortUrlItemList.module.css';
import ShortUrlItem from './ShortUrlItem';
import {useRouteLoaderData, Link} from 'react-router-dom'

export default function ShortUrlItemList({shortUrlItems}){
    const userData = useRouteLoaderData('root');

    return (
        <div className={classes['url-list-container']}>
            {(userData && userData.isLogin) && <Link to='new' className={classes.addButton}>Додати</Link>}
            <h2 className={classes['url-list-title']}>Мої посилання</h2>
            <ul className={classes['url-list']}>
                {shortUrlItems.map(item => (
                    <ShortUrlItem key={item.id} id={item.id} shortenedUrl={item.shortenedUrl} originalUrl={item.originalUrl} />
                ))}
            </ul>
        </div>
    );
}