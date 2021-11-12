import React from 'react';
import {Link} from 'react-router-dom';
import {makeStyles} from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    link: {
        textAlign: 'center',
        marginTop: 20
    },
    text: {
        textAlign: 'center',
        marginTop: 175,
    },
}));

export const ErrorView: React.FC = () => {
    const styles = useStyles();
    return (
        <>
            <h4 className={styles.text}>Something went wrong - Please try again.</h4>

            <div className={styles.link}>
                <Link className={styles.link} to="/">
                    Return to Home
                </Link>
            </div>
        </>
    );
};
