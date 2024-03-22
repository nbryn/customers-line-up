import React from 'react';
import type {ReactNode} from 'react';
import BootModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import makeStyles from '@mui/styles/makeStyles';

const useStyles = makeStyles(() => ({
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

export const BsModal: React.FC<Props> = ({
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
        <BootModal className={styles.modal} show={show} onHide={secondaryAction}>
            <BootModal.Dialog>
                <BootModal.Header>
                    <BootModal.Title>{title}</BootModal.Title>
                </BootModal.Header>

                <BootModal.Body>
                    <p>{text}</p>
                    {children}
                </BootModal.Body>

                <BootModal.Footer>
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
                </BootModal.Footer>
            </BootModal.Dialog>
        </BootModal>
    );
};
