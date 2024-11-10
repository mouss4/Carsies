'use server'

import { Auction, Pagedesult } from "@/types";

export async function getData(pageNumber: number, pageSize: number): Promise<Pagedesult<Auction>> {
    const res = await fetch(`http://localhost:6001/search?pageSize=${pageSize}&pageNumber=${pageNumber}`, {
        cache: 'force-cache',
      });

    if(!res.ok) throw new Error('Failed to fetch data');

    return res.json();
}
