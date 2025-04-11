import classes from './ShortUrlItemInfo.module.css';
import { Form, Link } from 'react-router-dom';

export default function ShortUrlItemInfo({ data }) {
    const formatDate = (isoString) => {
        const date = new Date(isoString);
        return date.toLocaleDateString('uk-UA', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    return (
        <div className={classes.container}>
            <div className={classes.urlGroup}>
                <div className={classes.field}>
                    <span className={classes.label}>Оригінальне посилання:</span>
                    <a 
                        href={data.originalUrl} 
                        target="_blank" 
                        rel="noopener noreferrer"
                        className={classes.value}
                        title={data.originalUrl}
                    >
                        {data.originalUrl.length > 60 
                            ? `${data.originalUrl.substring(0, 60)}...` 
                            : data.originalUrl}
                    </a>
                </div>
                
                <div className={classes.field}>
                    <span className={classes.label}>Скорочений код:</span>
                    <Link 
                        to={`/${data.shortenedUrl}`} 
                        className={classes.value}
                        title="Перейти за посиланням"
                    >
                        {data.shortenedUrl}
                    </Link>
                </div>
            </div>

            <div className={classes.metaGroup}>
                <div className={classes.metaItem}>
                    <span className={classes.metaLabel}>ID:</span>
                    <span className={classes.metaValue}>{data.id}</span>
                </div>
                
                <div className={classes.metaItem}>
                    <span className={classes.metaLabel}>Користувач:</span>
                    <span className={classes.metaValue}>{data.user.userName}</span>
                </div>
                
                <div className={classes.metaItem}>
                    <span className={classes.metaLabel}>Створено:</span>
                    <span className={classes.metaValue}>{formatDate(data.createdAt)}</span>
                </div>
            </div>

            <div className={classes.actions}>
                <Form 
                    method="delete"
                    action={`/short-url-delete/${data.id}`}
                >
                    <button 
                        type="submit"
                        className={classes.deleteButton}
                        title="Видалити запис"
                    >
                        🗑️ Видалити
                    </button>
                </Form>

                <Link 
                    to=".."
                    className={classes.backButton}
                    title="Повернутися до списку"
                >
                    Назад
                </Link>
            </div>
        </div>
    );
}