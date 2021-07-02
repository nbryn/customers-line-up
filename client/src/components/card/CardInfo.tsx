import React from 'react';
import Typography from '@material-ui/core/Typography';

import {Icons} from '../Icons';

type Icon = {
    text: string;
    icon: string;
    styles: string;
};

type Props = {
    icon1: Icon;
    icon2: Icon;
    icon3?: Icon;
};

export const CardInfo: React.FC<Props> = ({icon1, icon2, icon3}: Props) => {
    return (
        <>
            <Typography className={icon1.styles}>
                <Icons icon={icon1.icon} />
                {icon1.text}
            </Typography>
            <Typography className={icon2.styles}>
                <Icons icon={icon2.icon} />
                {icon2.text}
            </Typography>
            {icon3 && (
                <Typography className={icon3.styles}>
                    <Icons icon={icon3.icon} />
                    {icon3.text}
                </Typography>
            )}
        </>
    );
};
