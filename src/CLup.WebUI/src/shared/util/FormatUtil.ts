import type {TimeInterval} from '../../autogenerated';

const formatInterval = (interval: TimeInterval | undefined | null) =>
    `${(interval?.start as unknown as string).substring(0, 5)} - ${(
        interval?.end as unknown as string
    ).substring(0, 5)}`;

export default {
    formatInterval,
};
