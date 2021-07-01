import React, {ReactElement, useEffect, useState} from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';

import {DTO} from '../models/General';
import {Table, TableColumn} from '../components/Table';

export type Props = {
   actions: any;
   columns: TableColumn[];
   tableTitle: string | ReactElement;
   loading: boolean;
   fetchTableData: () => Promise<DTO[]>;
   tableData?: DTO[];
   removeEntryWithId?: string | null;
   emptyMessage?: string;
};

export const TableContainer: React.FC<Props> = ({
   actions,
   columns,
   tableTitle,
   loading,
   fetchTableData,
   removeEntryWithId,
   emptyMessage,
}: Props) => {
   const [tableData, setTableData] = useState<DTO[]>([]);

   useEffect(() => {
      (async () => {
         const tableData = await fetchTableData();

         setTableData(tableData);
      })();
   }, []);

   useEffect(() => {
      const updatedData = tableData.filter((b) => b.id !== removeEntryWithId);

      console.log(removeEntryWithId);

      setTableData(updatedData);
   }, [removeEntryWithId]);

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
