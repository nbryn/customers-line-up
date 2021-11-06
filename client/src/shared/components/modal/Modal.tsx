import React, {ReactNode} from 'react';
import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import {makeStyles} from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    modal: {
        top: 50,
    },
}));

type Props = {
    show: boolean;
    title?: string;
    text?: string;
    primaryDisabled?: boolean;
    primaryAction?: () => void;
    primaryActionText?: string;
    secondaryAction?: () => void;
    children?: ReactNode;
};

export const Modal: React.FC<Props> = ({
    show,
    title,
    text,
    primaryAction,
    primaryDisabled = false,
    primaryActionText,
    secondaryAction,
    children,
}: Props) => {
    const styles = useStyles();

    return (
        <>
            <BsModal className={styles.modal} show={show} onHide={secondaryAction}>
                <BsModal.Dialog>
                    <BsModal.Header>
                        <BsModal.Title>{title}</BsModal.Title>
                    </BsModal.Header>

                    <BsModal.Body>
                        <p>{text}</p>
                        {children}
                    </BsModal.Body>

                    <BsModal.Footer>
                        <Button variant="secondary" onClick={secondaryAction}>
                            Close
                        </Button>
                        {primaryAction && (
                            <Button
                                variant="primary"
                                onClick={primaryAction}
                                disabled={primaryDisabled}
                            >
                                {primaryActionText}
                            </Button>
                        )}
                    </BsModal.Footer>
                </BsModal.Dialog>
            </BsModal>
        </>
    );
};
