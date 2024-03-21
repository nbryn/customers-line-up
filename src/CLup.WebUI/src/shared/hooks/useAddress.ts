import {useEffect, useState} from 'react';

import AddressService from '../services/AddressService';
import type {Address} from '../services/AddressService';
import type {ComboBoxOption} from '../components/form/ComboBox';
import type {FormHandler} from './useForm';
import type {Index} from '../models/General';

export type AddressKey = 'zip' | 'street';

export type AddressHandler = {
    addresses: Address[];
    fetchAddresses: (zip: string | undefined) => Promise<void>;
    fetchZips: () => Promise<void>;
    getLabels: (key: AddressKey) => ComboBoxOption[];
};

export const useAddress = <T extends Index>(formHandler: FormHandler<T>): AddressHandler => {
    const [addresses, setAddresses] = useState<Address[]>([]);

    const fetchZips = async (): Promise<void> => {
        setAddresses(await AddressService.fetchZips());
    };

    const fetchAddresses = async (zip: string | undefined): Promise<void> => {
        if (zip) {
            const address = addresses.find((a) => a.zipCity === zip);
            const newAddresses = await AddressService.fetchAddresses(address);

            setAddresses(newAddresses);
        }
    };

    const getLabels = (key: AddressKey): ComboBoxOption[] => {
        return addresses.map((address) => ({
            label: key === 'zip' ? address.zipCity : address.street ?? 'Choose zip first',
        }));
    };

    useEffect(() => {
        (async () => {
            await fetchZips();
        })();
    }, []);

    useEffect(() => {
        (async () => {
            const {zip} = formHandler.values;
            await fetchAddresses(zip as string);
        })();
    }, [formHandler.values.zip]);

    return {
        addresses,
        fetchAddresses,
        fetchZips,
        getLabels,
    };
};
