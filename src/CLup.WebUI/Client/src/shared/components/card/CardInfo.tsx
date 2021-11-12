import React from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import {Icons} from '../Icons';

const useStyles = makeStyles((theme) => ({
    icon: {
        top: -2,
    },
    infoText: {
        marginBottom: 30,
        marginLeft: 25,
    },
}));

type InfoText = {
    text: string;
    icon: string;
};

type Props = {
    infoTexts: InfoText[];
};

export const CardInfo: React.FC<Props> = ({infoTexts}: Props) => {
    const styles = useStyles();
    return (
        <>
            {infoTexts.map((infoText) => {
                return (
                    <Row className={styles.infoText} key={infoText.icon}>
                        <Typography>
                            <Icons icon={infoText.icon}  />
                            {infoText.text}
                        </Typography>
                    </Row>
                );
            })}
        </>
    );
};
