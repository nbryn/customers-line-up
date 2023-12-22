import {Badge} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

const useStyles = makeStyles((theme) => ({
    header: {
        marginTop: 85,
        marginBottom: 35,
    },
}));

export enum HeaderSize {
    H1 = 'H1',
    H2 = 'H2',
    H3 = 'H3',
    H4 = 'H4',
}

export type HeaderProps = {
    text: string;
    size?: HeaderSize;
    className?: string
};

export const Header: React.FC<HeaderProps> = ({text, size, className}: HeaderProps) => {
    const styles = useStyles();

    switch (size) {
        case HeaderSize.H1:
            return (
                <h1 className={className ?? styles.header}>
                    <Badge variant="primary">{text}</Badge>
                </h1>
            );
        case HeaderSize.H2:
            return (
                <h2 className={className ?? styles.header}>
                    <Badge variant="primary">{text}</Badge>
                </h2>
            );
        case HeaderSize.H3:
            return (
                <h3 className={className ?? styles.header}>
                    <Badge variant="primary">{text}</Badge>
                </h3>
            );
        case HeaderSize.H4:
            return (
                <h4 className={className ?? styles.header}>
                    <Badge variant="primary">{text}</Badge>
                </h4>
            );
        default:
            return (
                <h1 className={styles.header}>
                    <Badge variant="primary">{text}</Badge>
                </h1>
            );
    }
};
