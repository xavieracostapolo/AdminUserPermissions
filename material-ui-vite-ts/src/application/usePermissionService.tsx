import { useQuery } from '@tanstack/react-query'
import { getPermission } from '../infraestructure'

const key: any = 'permissions'

export const useGetPermission = () => {
    return useQuery({ queryKey: [key], queryFn: getPermission })
}
