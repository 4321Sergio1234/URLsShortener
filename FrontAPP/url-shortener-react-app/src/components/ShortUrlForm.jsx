import { Form , Link} from 'react-router-dom';
import classes from './ShortUrlForm.module.css';


export default function ShortUrlForm() {
    
    return (
        <div className={classes.formContainer}>
            <h2>Створити нове коротке посилання</h2>
            <Form
                method="post"  
                className={classes.form}
            >
                <div className={classes.formGroup}>
                    <label htmlFor="originalUrl" className={classes.label}>
                        Оригінальний URL:
                    </label>
                    <input
                        type="url"
                        id="originalUrl"
                        name="originalUrl"
                        className={classes.input}
                        placeholder="https://example.com"
                        required
                        pattern="https?://.+"
                    />
                </div>
                <div className={classes.formActions}>
                    <button type="submit" className={classes.submitButton}>
                        Створити
                    </button>
                </div>
            </Form>
            <Link 
                    to=".."
                    className={classes.backButton}
                    title="Повернутися до списку"
                >
                    Назад
            </Link>
        </div>
    );
}