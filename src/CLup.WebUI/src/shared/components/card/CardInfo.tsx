import React from 'react';
import Typography from '@mui/material/Typography';
import {Grid} from '@mui/material';

import {Icons} from '../Icons';

type InfoText = {
    text: string;
    icon: string;
};

type Props = {
    infoTexts: InfoText[];
};

export const CardInfo: React.FC<Props> = ({infoTexts}: Props) => {
    return (
        <Grid container spacing={2} flexDirection="column">
            {infoTexts.map((infoText) => {
                return (
                    <Grid item xs={12} key={infoText.icon}>
                        <Typography textAlign="left">
                            <Icons icon={infoText.icon} />
                            {infoText.text}
                        </Typography>
                    </Grid>
                );
            })}
        </Grid>
    );
};
