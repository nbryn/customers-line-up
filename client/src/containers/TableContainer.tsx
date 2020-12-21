import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';

import {DTO} from '../models/General';
import {Table, TableColumn} from '../components/Table';


export type Props = {
   actions: any;
   columns: TableColumn[];
   tableTitle: string;
   fetchTableData: () => Promise<DTO[]>;
   tableData?: DTO[];
   removeEntryId?: number | null;
   emptyMessage?: string;
};

export const TableContainer: React.FC<Props> = ({
   actions,
   columns,
   tableTitle,
   fetchTableData,
   removeEntryId,
   emptyMessage,
}: Props) => {
   const [loading, setLoading] = useState<boolean>(true);
   const [tableData, setTableData] = useState<DTO[]>([]);

   useEffect(() => {
      (async () => {
         const tableData = await fetchTableData();

         setTableData(tableData);
         setLoading(false);
      })();
   }, []);

   useEffect(() => {
      const updatedData = tableData.filter((b) => b.id !== removeEntryId);

      setTableData(updatedData);
   }, [removeEntryId]);

   return (
      <>
         {loading ? (
            <CircularProgress />
         ) : (
            <Table
               actions={actions}
               columns={columns}
               data={tableData}
               title={tableTitle}
               emptyMessage={emptyMessage}
            />
         )}
      </>
   );
};
